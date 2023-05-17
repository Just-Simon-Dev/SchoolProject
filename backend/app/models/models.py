from sqlmodel import Field, SQLModel
from datetime import datetime
from typing import Optional

class Cities(SQLModel, table=True):
    id: Optional[int] = Field(default=None, primary_key=True)
    postal_code: str
    name: str
    lat: float
    lon: float