import { Hono } from 'hono';
import { generateBackgroundImage } from "./bgImageGenerator.ts";


const app = new Hono();

app.get('/', async (c) => {
  const img = await generateBackgroundImage(1024, 768, 33);
  return c.body(img, {
    headers: {
      "Content-Type": "image/png"
    }
  });
})

await Deno.serve({port: 8787}, app.fetch);