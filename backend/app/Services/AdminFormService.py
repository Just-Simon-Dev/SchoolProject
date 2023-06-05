from fastapi import Request
from sqlalchemy.orm import Session

from app.models.AdminFormModel import AdminFormModel
from app.models import models
import bcrypt

class AdminFormService():
    def __init__(self, db: Session):
        self.db = db

    def checkCorrectCredentials(self, adminForm: AdminFormModel):
        results = self.db.query(models.Login).filter_by(login=adminForm.login).all()
        for credential in results:
            credential:models.Login
            if bcrypt.checkpw(adminForm.password, credential.hashed_password):
                return credential
        return "incorrect login or password"
