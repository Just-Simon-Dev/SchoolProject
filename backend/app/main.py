from fastapi import FastAPI, Depends, Request, Form
from sqlalchemy.orm import Session

from app.models.StudentFormModel import StudentFormModel
from .Services.DbService import SessionLocal, engine
from .Services.StudentFormService import StudentFormService
from .models import models
from datetime import date


models.Base.metadata.create_all(engine)

app = FastAPI(root_path='/api')

# Dependency
def get_db():
    db = SessionLocal()
    try:
        yield db
    finally:
        db.close()

@app.post("/StudentForm")
async def root(
    request: Request, 
    name_and_surname: str = Form(),
    student_class: str = Form(),
    date: date = Form(),
    numberOfLessons: int = Form(),
    numberOfHour: str = Form(),
    issues: str = Form(),
    db: Session = Depends(get_db)
    ):
    student_form_model = StudentFormModel(name_and_surname, student_class, date, numberOfLessons, numberOfHour, issues)
    student_form_service = StudentFormService(db)
    ip = student_form_service.sendDataToDb(request, student_form_model)
    return ip