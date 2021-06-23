from cryptography.hazmat.primitives import serialization
from cryptography.hazmat.primitives import hashes
from cryptography.hazmat.primitives.asymmetric import rsa
from cryptography.hazmat.primitives.asymmetric import padding
from cryptography.hazmat.backends import default_backend
from cryptography.exceptions import InvalidSignature

class Crypto:
    private_key = None
    public_key = None

    def __init__(self):
        try:
            file = open(f"keys/private_key.pem", 'rb')
            pemlines = file.read()
            self.private_key = serialization.load_pem_private_key(pemlines, None, default_backend())
            file.close()
        except IOError:
            self.GeneratePrivateKey()

        try:
            file = open(f'keys/public_key.pem', 'rb')
            pemlines = file.read()
            self.public_key = serialization.load_pem_public_key(pemlines, default_backend())
            file.close()
        except IOError:
            self.GeneratePublicKey()

    def GeneratePublicKey(self):
        try:
            file = open(f"keys/public_key.pem")
            print('Public key already exists... skipping generation')
            file.close()
        except IOError:
            try:
                file1 = open('keys/private_key.pem', 'rb')
                pemlines = file1.read()
                private_key = serialization.load_pem_private_key(pemlines, None, default_backend())
                file1.close()
                
                public_key = private_key.public_key()
                pem = public_key.public_bytes(
                    encoding=serialization.Encoding.PEM,
                    format=serialization.PublicFormat.SubjectPublicKeyInfo
                )
                file = open(f'keys/public_key.pem', 'wb')
                file.write(pem)
                file.close()

                file = open(f'keys/public_key.pem', 'rb')
                pemlines = file.read()
                self.public_key = serialization.load_pem_public_key(pemlines, default_backend())
                file.close()

            except IOError:
                self.GeneratePrivateKey()
                self.GeneratePublicKey()

    def GeneratePrivateKey(self):
        try:
            file = open(f"keys/private_key.pem")
            print('Private key already exists... Skipping generation')
            file.close()
        
        except IOError:
            private_key = private_key = rsa.generate_private_key(
                public_exponent=65537,
                key_size=2048,
                backend=default_backend()
            )

            pem = private_key.private_bytes(
                encoding=serialization.Encoding.PEM,
                format=serialization.PrivateFormat.PKCS8,
                encryption_algorithm=serialization.NoEncryption()
            )
            
            file = open(f"keys/private_key.pem", "wb")
            file.write(pem)
            file.close()
            
            file = open(f"keys/private_key.pem", "rb")
            pemlines = file.read()
            self.private_key = serialization.load_pem_private_key(pemlines, None, default_backend())
            file.close()
        
    def EncryptMessage(self, msg):
        encrypted = self.public_key.encrypt(
            msg,
            padding.OAEP(
                mgf=padding.MGF1(algorithm=hashes.SHA256()),
                algorithm=hashes.SHA256(),
                label=None
            )
        )
        return encrypted

    def SignMessage(self, msg):
        signed = self.private_key.sign(
            msg,
            padding.PSS(
                mgf=padding.MGF1(hashes.SHA256()),
                salt_length=padding.PSS.MAX_LENGTH
            ),
            hashes.SHA256()
        )
        return signed
        
    def DecryptMessage(self, msg):
        plaintext = self.private_key.decrypt(
            msg,
            padding.OAEP(
                mgf=padding.MGF1(algorithm=hashes.SHA1()),
                algorithm=hashes.SHA1(),
                label=None
            )
        )
        return plaintext

    def Verify(self, msg, signature):
        try:
            self.public_key.verify(
                signature,
                msg,
                padding.PSS(
                    mgf=padding.MGF1(hashes.SHA256()),
                    salt_length=padding.PSS.MAX_LENGTH
                ),
                hashes.SHA256()
            )
            print('signatures match')
        except InvalidSignature:
            print('signatures do not match!')

    def GetPublicKey(self):
        try:
            file = open('keys/public_key.pem', 'rb')
            pem = file.read()
            return pem
        except IOError:
            print('Can\'t open file')