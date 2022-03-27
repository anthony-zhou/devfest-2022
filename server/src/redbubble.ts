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

export async function loadPosterImageUrl(url: string) {
  const browser = await puppeteer.launch({
    'args' : [
      '--no-sandbox',
      '--disable-setuid-sandbox',
    ],
  });
  const page = await browser.newPage();

  await page.goto(url);
  await page.waitForSelector('#app > div > div.ds-theme-find-your-thing.App__dsWrapper--RyVET > main > div > div > div:nth-child(2) > div > div.DesktopProductPage__primaryContent--2GiWp > div.DesktopProductPage__left--1anl7 > div:nth-child(1) > div.PreviewGallery__gallery--1mxHG.DesktopProductPage__gallery--36AFk > div.PreviewGallery__leftColumn--1ut9H > div:nth-child(2) > div > div.PreviewGallery__preview--bWOCE.GalleryImage__preview--Hx82M > img');
  const data = await page.evaluate(() => {
    const image = document.querySelectorAll('img.GalleryImage__img--12Vov')[1] as HTMLImageElement;
    return image.src;
  });

  return data;
}