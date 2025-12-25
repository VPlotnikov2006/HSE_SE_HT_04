CREATE USER orders_user WITH PASSWORD 'orders_pwd';
CREATE USER pay_user WITH PASSWORD 'pay_pwd';

CREATE DATABASE orders OWNER orders_user;
CREATE DATABASE payments OWNER pay_user;
