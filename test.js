import http from 'k6/http';
import { check, sleep } from 'k6';

export const options = {
  vus: 100,
  duration: '1m',
};

const BASE_URL = 'http://localhost:9003';
export default () => {
  const id = Math.floor(Math.random() * 1000000);
  http.get(`${BASE_URL}/tick/${id}`, {
    tags: {
      name: "Ticks"
    },
  });
  //sleep(1);
};
