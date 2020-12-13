import random
import socket
import time
from thread import *
import threading
from datetime import datetime
import json
import requests

clients_lock = threading.Lock()
connected = 0

clients = {}
games = []
waitingPlayers = []

urlLogIn = "https://0z00p0yvy8.execute-api.us-east-2.amazonaws.com/default/loginPlayer"
urlSignUp = "https://ecrp5cn9vg.execute-api.us-east-2.amazonaws.com/default/registerPlayer"
urlUpdate = "https://q93u22k2al.execute-api.us-east-2.amazonaws.com/default/chessUpdate"
signUpQueryParams = {'username' : 'username'}, {'password' : 'password'}
updateQueryParams = {'username' : 'username'}, {'wins' : 'w'}, {'losses' : 'l'}

class Game:
   gameID = 0
   #white and black will be the clients.
   white = ""
   black = ""
   currentTurn = "white"

   def __init__(self, w, b, id):
      self.white = w
      self.black = b
      self.gameID = id

   def StartGame(self, sock):
      message = {"cmd": 7}
      d = json.dumps(message)
      sock.sendto(d, self.white)
      message = {"cmd": 8}
      d = json.dumps(message)
      sock.sendto(d, self.black)

      print("STARTING GAME!!!!!!!!!!!!")
      
      


def MatchMake(sock):
   if(len(waitingPlayers) >= 2):
      g = Game(waitingPlayers[0], waitingPlayers[1], len(games))
      print("CreatingGame")
      waitingPlayers.remove(g.white)
      waitingPlayers.remove(g.black)
      games.append(g)
      g.StartGame(sock)
      if(len(waitingPlayers) >= 2):
         MatchMake()
      

def connectionLoop(sock):
   while True:
      data, addr = sock.recvfrom(1024)
      data = str(data)
      if 'connect' in data:
         print("Connect Received")
      if 'heartbeat' in data:
         print("Heartbeat Received")
         #print(clients)
         #print(addr)
         #print(clients[addr])
         
      if 'chess' in data:
         print('move received')
         m = json.loads(data)
         Move = {"cmd": 3, "pieceID": m['pieceID'], "x": m['x'], "y": m['y']}
         d = json.dumps(Move)
         for g in games:
            if g.white == addr:
               sock.sendto(d, g.black)
            if g.black == addr:
               sock.sendto(d, g.white)
         data = ""
         #sock.sendto(bytes(d, 'utf8'), 0, addr)
         #sock.sendto(d, addr)

      if 'signin' in data:
         print('logging in')
         response = requests.get(urlLogIn)
         responseBody = json.loads(response.content)

         m = json.loads(data)
         if m['username'] in responseBody:
            if responseBody[m['username']]['password'] == m['password']:
               message = {"cmd": 5}
               d = json.dumps(message)
               sock.sendto(d, addr)
               waitingPlayers.append(addr)
               print(len(waitingPlayers) + "players waiting...")
               MatchMake(sock)

         # this would be the full database being sent to clients then have client hold struct that
         # holds username pwassword and for each loop through it to see if the user and pass 
         # is equal to anything in the databse sent to client

      if 'signup' in data:
         print("signing up")
         m = json.loads(data)
         newURL = urlSignUp + "?username=" + m['username'] + "&password=" + m['password']
         response = requests.get(newURL)
         message = {"cmd": 5}
         d = json.dumps(message)
         sock.sendto(d, addr)
         waitingPlayers.append(addr)
         print(str(len(waitingPlayers)) + "players waiting...")
         MatchMake(sock)

        


      if addr in clients:
         if 'heartbeat' in data:
            clients[addr]['lastBeat'] = datetime.now()
            #print("HeartbeatReceived")
         #elif 'chessMove' in data:
            #print('move received')
            # = json.loads(message)
            #Move = {"cmd": 3, "pieceID": m['pieceID'], "x": m['x'], "y": m['y']}
            #d = json.dumps(Move)
            #for c in clients:
            #   sock.sendto(bytes(d, 'utf8'), c[0], c[1])
      else:
         #if 'connect' in data:
         print("connection established!")
         clients[addr] = {}
         clients[addr]['lastBeat'] = datetime.now()
         clients[addr]['id'] = addr
         idMessage = {"cmd" : 0, "id": "I don't even know anymore."}
         im = json.dumps(idMessage)
         #for c in clients:
         #      sock.sendto(bytes(im,'utf8'), (c[0],c[1]))
         #sock.sendto(bytes(im, 'utf-8'), (clients[addr][0], clients[addr][1]))

      

def cleanClients():
   while True:
      print (len(clients))
      for c in list(clients.keys()):
         if(datetime.now() - clients[c]['lastBeat']).total_seconds() > 5:
            print("Dropped client")
            clients_lock.acquire()
            del clients[c]
            clients_lock.release()
      time.sleep(1)

def gameLoop(sock):
   while True:
      GameState = {"cmd": 1, "players": []}
      clients_lock.acquire()
      print (len(clients))
      for c in clients:
         player = {}
        
         GameState['players'].append(player)
      s=json.dumps(GameState)
      print(s)
      for c in clients:
         sock.sendto(bytes(s,'utf8'), (c[0],c[1]))
      clients_lock.release()
      time.sleep(1)

def main():
   port = 12345
   s = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
   s.bind(('', port))
   #start_new_thread(gameLoop, (s,))
   start_new_thread(connectionLoop, (s,))
   start_new_thread(cleanClients,())
   while True:
      time.sleep(1)

if __name__ == '__main__':
   main()