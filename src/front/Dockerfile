#### STAGE 1
FROM node:lts-alpine as builder
RUN mkdir -p /usr/src/app

WORKDIR /usr/src/app

COPY package*.json ./
RUN npm ci
COPY . .
RUN npm run build --prod


#### STAGE 2
FROM nginx:1.19

COPY --from=builder /usr/src/app/dist/front /usr/share/nginx/html/
COPY --from=builder /usr/src/app/.docker/nginx.conf /etc/nginx/nginx.conf

EXPOSE 80

# Start the Nginx server
CMD ["nginx", "-g", "daemon off;"]

