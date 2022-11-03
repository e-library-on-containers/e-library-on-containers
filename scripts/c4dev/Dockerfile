FROM timbru31/java-node:11-alpine-jdk
WORKDIR /app/c4

RUN npm i -g c4builder

COPY c4builder .

CMD ["c4builder", "site"]

