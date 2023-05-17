from sqlalchemy import create_engine
from sqlmodel import SQLModel, create_engine
from ..models import models

DATABASE_URL = 'postgresql://admin:admin!@postgres:5432/evidention'

print(DATABASE_URL)

engine = create_engine(DATABASE_URL)

def create_db_and_tables():
    SQLModel.metadata.create_all(engine)