FROM alpine:3.15

# Install required packages
RUN apk add --update --no-cache bash dos2unix

ENV PYTHONUNBUFFERED=1
RUN apk add --update --no-cache python3 && ln -sf python3 /usr/bin/python
RUN python3 -m ensurepip

WORKDIR /scripts

COPY ./requirements.txt /scripts/requirements.txt

RUN pip3 install --no-cache-dir --upgrade -r /scripts/requirements.txt

# Copy files
COPY jobs/*.* ./jobs/
COPY crontab.* ./
COPY start.sh .

# Fix line endings && execute permissions
RUN dos2unix crontab.* *.sh jobs/*.* \
    && \
    find . -type f -iname "*.sh" -exec chmod +x {} \
    && \
    find . -type f -iname "*.py" -exec chmod +x {} \;

RUN touch /var/log/cron.log

# Run cron on container startup
CMD ["./start.sh"]