FROM node:18.12.1

WORKDIR /frontend

COPY package.json ./

RUN npm i

EXPOSE 8080

CMD ["npm", "run", "serve"]