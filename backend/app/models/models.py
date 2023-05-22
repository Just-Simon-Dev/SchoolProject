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

class Form(Base):
    __tablename__ = "form"
    id = Column(Integer, primary_key=True)
    full_name = Column(String)
    date = Column(Date)
    time = Column(String)
    issues = Column(String)
    ip = Column(String, ForeignKey("identifaction.ip"))





