import { serveDir } from "jsr:@std/http/file-server";

Deno.serve((req) => {
  const url = new URL(req.url);

  if (url.pathname === "/api/hello") {
    return new Response(JSON.stringify({ message: "Hello API" }), {
      headers: { "content-type": "application/json" },
    });
  }

  return serveDir(req, { fsRoot: ".", showDirListing: true });
});