from fastapi import Request
from sqlalchemy.orm import Session

from app.models.StudentFormModel import StudentFormModel
from app.models import models

class StudentFormService():
    def __init__(self, db: Session):
        self.db = db

    def sendDataToDb(self, request:Request, student_form_model:StudentFormModel):
        ip = request.client.host
        print(ip)
        
        form = models.Form(student_form_model.name_and_surname, student_form_model.date, student_form_model.numberOfHour, student_form_model.issues, ip)

        self.db.add(form)
        self.db.commit()
        self.db.close()