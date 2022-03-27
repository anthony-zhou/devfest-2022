import express from 'express';
import * as redbubble from './redbubble';

const app = express();
const port = 3000;

app.get('/', (req, res) => {
  res.send('Hello there!');
});

app.get('/search/:query', async (req, res) => {
  const { query } = req.params;
  const urls = await redbubble.search(query);
  
  res.send(urls.map(url => `<img src = "${url}" />`).join(''));
});

app.listen(port, () => {
  console.log(`Example app listening on port ${port}`);
});
