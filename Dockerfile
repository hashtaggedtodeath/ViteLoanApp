FROM node:20-alpine

# Рабочая директория
WORKDIR /app

# Копируем package.json и package-lock.json
COPY package*.json ./

# Устанавливаем зависимости
RUN npm install

# Копируем весь фронтенд
COPY . .

# Стартовый скрипт (например для Vite)
CMD ["npm", "run", "dev", "--", "--host"]
