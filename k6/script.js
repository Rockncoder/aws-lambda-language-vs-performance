import http from 'k6/http';
import { sleep } from 'k6';

const csharpUrl = 'https://rsi8v8tig4.execute-api.us-west-2.amazonaws.com/vehicles';
const goUrl = 'https://ac49s93wuf.execute-api.us-west-2.amazonaws.com/vehicles';
const nodeUrl = 'https://udr0n0z8sj.execute-api.us-west-2.amazonaws.com/vehicles';
const pythonUrl = 'https://7wl8lx6jjg.execute-api.us-west-2.amazonaws.com/vehicles';
const urls = [csharpUrl, goUrl, nodeUrl, pythonUrl];

export let options = {
  vus: 5,
  duration: '30s',
  // threshold: {
  //   http_req_duration: ['p(95)<1500']
  // }
}

export default function () {
  http.get(urls[3]);
  sleep(1);
}
