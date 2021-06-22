from sockets import Socket
import _thread
import threading

server = Socket(60000)

lock = threading.Lock()

while True:
    (connection, client) = server.s.accept()
    lock.acquire()
    _thread.start_new_thread(server.ClientThread, (connection,lock))

server.s.close()


