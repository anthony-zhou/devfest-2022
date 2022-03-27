import puppeteer from 'puppeteer';

export async function search(query: string, limit: number = 5) {
  const browser = await puppeteer.launch();
  const page = await browser.newPage();

  await page.goto(`https://www.redbubble.com/shop/?query=${encodeURIComponent(query)}&iaCode=u-print-poster`);
  const urls: string[] = [];
  for (let i = 1; i < i + limit; i++) {
    const element = await page.waitForSelector(`#SearchResultsGrid > a:nth-child(${i}) > div > div.styles__box--2Ufmy.styles__imageContainer--1DSGW.styles__roundedCorners--2unv1 > div > div.styles__box--2Ufmy > div > div > img`);
    const url = await page.evaluate((e) => e.src, element);
    urls.push(url);
  }

  return urls;
}