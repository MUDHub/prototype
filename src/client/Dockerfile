FROM node:alpine AS builder

WORKDIR /home/node/app
COPY package*.json ./
RUN npm install
COPY . .
RUN npm run build -- --prod



FROM nginx:alpine AS runner
COPY --from=builder /home/node/app/dist/ /usr/share/nginx/html
