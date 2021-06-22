import socket;
import json;
import hashlib
from mycrypt.crypt import Crypto

crypto = Crypto()

# message = b"Hey Man!!"
# encrypted = crypto.EncryptMessage(message)
# signed = crypto.SignMessage(message)
# print(signed)

# crypto.Verify(message, signed)

# decrypted = crypto.DecryptMessage(signed)
# print(decrypted.decode())

class Socket:

    s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

    def __init__(self, port):
        self.s.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
        self.s.bind(('', port))
        self.s.listen(5)

    def Login(self, username):
        print("Opening: " + username)
        try:
            file = open(f"players/{username}.json")
            data = json.load(file)
            msg = json.dumps(data)
            print(f"Sending {username}\'s data.")
        except IOError:
            msg = "not_found"
            print("Player doesn't exist")

        return msg

    def CreateAccount(self, account):
        newP = json.loads(account)
        try:
            file = open(f"players/{newP['username']}.json")
            msg = "already_exists"
            print('Player already exists.')
        except IOError:
            file = open(f"players/{newP['username']}.json", "w")
            file.write(json.dumps(newP, sort_keys=True, indent=4))
            msg = "player_created"
            print("New player created.")

        file.close()
        
        return msg


    def ClientThread(self, connection, lock):
        while True:
            ff = connection.recv(1024)
            ffstr = ff.decode('ascii')

            if ffstr.startswith("Open"):
                ffstr = ffstr[4:]
                toSend = self.Login(ffstr).encode('ascii')
                connection.sendall(toSend)
                sig_req = connection.recv(1024)
                connection.sendall(crypto.SignMessage(toSend))


            if ffstr.startswith("Create"):
                ffstr = ffstr[6:]
                connection.sendall(self.CreateAccount(ffstr).encode('ascii'))

            connection.close()
            lock.release()
            break