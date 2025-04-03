import { serve } from "https://deno.land/std/http/server.ts";
import { serveFile } from "https://deno.land/std/http/file_server.ts";

// In-memory todos for demo
const todos = [];

// Handle different routes
async function handler(req) {
    const url = new URL(req.url);
    
    // Serve static files
    if (url.pathname.startsWith('/static/')) {
        return await serveFile(req, '.' + url.pathname);
    }

    // API endpoints
    if (url.pathname === '/api/todos' && req.method === 'POST') {
        const formData = await req.formData();
        const text = formData.get('text');
        todos.push({ id: Date.now(), text: text, done: false });
        
        // Return formatted HTML instead of JSON
        const todoHtml = todos.map(todo => 
            `<div class="todo-item">
                <input type="checkbox" ${todo.done ? 'checked' : ''}>
                ${todo.text}
            </div>`
        ).join('');
        
        return new Response(todoHtml, {
            headers: { 'Content-Type': 'text/html' }
        });
    }

    if (url.pathname === '/api/todos' && req.method === 'GET') {
        // Return formatted HTML for GET requests too
        const todoHtml = todos.map(todo => 
            `<div class="todo-item">
                <input type="checkbox" ${todo.done ? 'checked' : ''}>
                ${todo.text}
            </div>`
        ).join('');
        
        return new Response(todoHtml, {
            headers: { 'Content-Type': 'text/html' }
        });
    }

    // Page routes
    if (url.pathname === '/todos') {
        const html = await Deno.readTextFile('../frontend_htmx/pages/todos.html');
        return new Response(html, {
            headers: { 'Content-Type': 'text/html' }
        });
    }

    // Default route (home page)
    const html = await Deno.readTextFile('../frontend_htmx/pages/index.html');
    return new Response(html, {
        headers: { 'Content-Type': 'text/html' }
    });
}

console.log("Server running on http://localhost:8000");
await serve(handler, { port: 8000 });
