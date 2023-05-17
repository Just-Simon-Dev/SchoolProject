from fastapi import FastAPI
from .Services.DbService import create_db_and_tables

app = FastAPI(root_path='/api')

@app.on_event("startup")
def on_startup():
    create_db_and_tables()

@app.get("/")
async def root():
    return {"message": "Hello World"}