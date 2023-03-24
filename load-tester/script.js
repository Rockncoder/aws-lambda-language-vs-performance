import http from 'k6/http';
import { sleep } from 'k6';

export let options = {
  vus: 25,
  duration: '10s',
  threshold: {
    http_req_duration: ['p(95)<1500']
  }
}

export default function () {
  // http.get('https://ac49s93wuf.execute-api.us-west-2.amazonaws.com/vehicles');
  http.get('https://udr0n0z8sj.execute-api.us-west-2.amazonaws.com/vehicles');
  sleep(1);
}
