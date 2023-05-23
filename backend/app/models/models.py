from typing import Any
from sqlalchemy import Column, ForeignKey, Integer, String, Date
from sqlalchemy.orm import relationship

from ..Services.DbService import Base

class Login(Base):
    __tablename__ = "login"
    id = Column(Integer, primary_key=True)
    login = Column(String, unique=True)
    hashed_password = Column(String)

class Identifaction(Base):
    __tablename__ = "identifaction"
    ip = Column(String, primary_key=True)
    room_number = Column(String)
    computer_number = Column(Integer)
    def __init__(self, ip, room_number, computer_number):
         self.ip = ip
         self.room_number = room_number
         self.computer_number = computer_number

class Form(Base):
    __tablename__ = "form"
    id = Column(Integer, primary_key=True)
    full_name = Column(String)
    date = Column(Date)
    time = Column(String)
    issues = Column(String)
    ip = Column(String, ForeignKey("identifaction.ip"))
    def __init__(self, full_name, date, time, issues, ip):
          self.full_name = full_name
          self.date = date
          self.time = time
          self.issues = issues
          self.ip = ip





