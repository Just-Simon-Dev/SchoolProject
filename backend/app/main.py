from fastapi import FastAPI
from .Services.DbService import SessionLocal, engine
from .models import models


models.Base.metadata.create_all(engine)

app = FastAPI(root_path='/api')

# Dependency
def get_db():
    db = SessionLocal()
    try:
        yield db
    finally:
        db.close()


@app.get("/")
async def root():
    return {"message": "Hello World"}