import express from 'express';
import * as redbubble from './redbubble';

const app = express();
const port = process.env.PORT || 8080;

app.get('/', (req, res) => {
  res.send('Hello there!');
});

// Return Redbubble search results as JSON.
app.get('/search/:query', async (req, res) => {
  const { query } = req.params;
  const data = await redbubble.search(query);
  
  res.send(data);
});

// Given an item URL, return the image URL.
app.get('/image/:posterUrl', async (req, res) => {
  const { posterUrl } = req.params;
  const url = await redbubble.loadPosterImageUrl(posterUrl);
  res.send(url);
});

app.listen(port, () => {
  console.log(`Redbubble API listening on port ${port}`);
});
