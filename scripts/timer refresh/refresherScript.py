import datetime
import time
import webbrowser as web
from sqlalchemy import create_engine

sleepTime = 5 #in minutes
DATABASE_URL = 'postgresql://admin:admin!@postgres:5432/evidention'
engine = create_engine(DATABASE_URL)


while (True):
    currentDate = datetime.date.today()
    getTime = engine.connect().execute(f"SELECT time FROM form WHERE date= {currentDate}")
    getTime = getTime.split("-")[1]

    now = datetime.now()
    currentTime = now.strftime("%H:%M")

    if getTime < currentTime:
        web.open("https://www.google.pl")
    time.sleep(sleepTime*60)

