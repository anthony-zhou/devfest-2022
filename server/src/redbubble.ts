import puppeteer from 'puppeteer';

export async function search(query: string, limit: number = 10) {
  const browser = await puppeteer.launch({
    'args' : [
      '--no-sandbox',
      '--disable-setuid-sandbox',
    ],
  });
  const page = await browser.newPage();

  await page.goto(`https://www.redbubble.com/shop/?query=${encodeURIComponent(query)}&iaCode=u-print-poster`);
  const data = await page.evaluate((n) => 
    Array(n)
      .fill(0)
      .map((_, i) => document.querySelector(`#SearchResultsGrid > a:nth-child(${i + 1})`) as HTMLLinkElement)
      .map((e) => ({
        thumbnail: e.querySelector('img')?.src,
        itemUrl: e.href,
        title: e.querySelector('div > div.styles__box--2Ufmy.styles__display-flex--2Ww2j > div > div.styles__box--2Ufmy.styles__disableLineHeight--6s16u.styles__paddingRight-0--1YNvL > span')?.textContent,
        creator: e.querySelector('div > div.styles__box--2Ufmy.styles__display-flex--2Ww2j > div > div.styles__box--2Ufmy.styles__disableLineHeight--6s16u.styles__paddingRight-0--1YNvL > div > span')?.textContent,
      })), 
  limit);

  return data;
}