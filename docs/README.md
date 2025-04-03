<h1 align="center" style="font-weight: bold;">🏈 ABBA League Manager 🏈</h1>

<p align="center">
<a href="#tech">Technologies</a>
<a href="#prerequisites">Kicking it off!</a>
<a href="#routes">API Endpoints</a>
<a href="#colab">Collaborators</a>
<a href="#contribute">Contribute</a>
</p>


<p align="center">A Blood Bowl team, league and tournament manager with a versatile frontend and independent API that allows coaches to play matches in real time.</p>


<p align="center">
<a href="https://www.github.com/xarop-pa-toss/ABBA-LM">📱 <s>Try it out!</s> (soon)</a>
</p>
<p align="center">
  <a href="https://gist.github.com/xarop-pa-toss/0c54592cbdc52ba396838d428d766788">API Roadmap</a>
</p>  
<p align="center">
  <a href="https://gist.github.com/xarop-pa-toss/5518357d130d0aa33bf871b46a12957f">BloodTourney Roadmap</a>
</p>  

<h2 id="tech">🪛 What makes it tick!</h2>

The project is rather ambitious in regard to technologies used since it was my main objective to learn and apply new concepts as much as possible, within scope of course.
The final product will have implemented the following:

- [HTMX](https://htmx.org) frontend with some AlpineJS sprinkled in. The objective is to have it run as a webapp on mobile as well, either by building as a PWA or by using something like Hyperview. Will decide when I get to this part... 😊
- [Deno 2.0](https://deno.com) with JS is used for the backend with as little unnecessary additions as I can avoid putting in. Get request from frontend, get data from API, serve HTMX to the frontend. That's it (for now).
- ASP.NET Web API with MongoDB that can serve any frontend client with:
  - JWT and OAuth2 authentication and password hashing/data validation
  - Proper CORS policy to serve different frontend clients
  - Role-based authorization for _admin_ and _user_
  - Customized exception handling
  - Logging visible in-app to admin accounts in a clean, readable format
  - RESTful API based endpoints with as much input sanitization as I can squeeze into it 
  - Organized and easy to work with API documentation using Scalar
  - other stuff... eventually
  
The backend follows a Repository pattern, and the MongoDB database is easy to figure out and can have an enforced schema (provided in the files as well as a default version of the DB). Could it be done with SQL? Absolutely... but as with everything in this project, I wanted to try out new stuff.

The Tournament management part of the app is setup as a C# code library that is used by the main API, but isn't an integral part of it. I made it this way because I wanted it to stand alone and be usable elsewhere.
<h2 id="prerequisites">🏈 Kicking it off!</h3>

### Prerequisites
All project dependencies can be installed using our interactive installation script:

```bash
cd dev_install
chmod +x install_dependencies.sh
./install_dependencies.sh
```

The script allows you to install:
1. API dependencies only
2. Web dependencies only 
3. Both API and Web dependencies

### Manual Installation

#### API Setup
1. Install [.NET 9.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
2. Install [MongoDB 7.0+](https://www.mongodb.com/try/download/community)
3. Navigate to the API directory and run:
```bash
dotnet restore
dotnet build
dotnet run
```

#### Web Setup
1. Install Deno 2.0+:
```bash
# Windows
irm https://deno.land/install.ps1 | iex

# MacOS/Linux
curl -fsSL https://deno.land/install.sh | sh
```

2. Verify installation:
```bash
deno --version
```

3. Navigate to web directory and run:
```bash
deno task start
```

### Environment Configuration

Use the `.env.example` as reference to create your configuration file `.env` with your AWS Credentials

```yaml
NODE_AWS_REGION=us-east-1
NODE_AWS_KEY_ID={YOUR_AWS_KEY_ID}
NODE_AWS_SECRET={YOUR_AWS_SECRET}
```

<h2 id="routes">❓ API Endpoints</h2>

[The API is entirely documented with Scalar](https://www.github.com/xarop-pa-toss/ABBA-LM)  which doubles as a fantastic way to test the endpoints too! If you've ever used Swagger and Postman, it feels (to me at least) as a mix of both.
It supports JWT authentication and is implemented with with POST and GET request that attempt to create a new token and get current user info, respectively.

All base CRUD operations can be performed on all entities as part of the base MongoRepository class, from which entity specific repository classes derive.

<h2 id="colab">🤝 Collaborators</h2>

<h2 id="contribute">📫 Contribute</h2>
Start by cloning the repo to your machine:

`git clone https://github.com/xarop-pa-toss/ABBA-LM`

Anyone can contribute by adding Issues, creating branches and making pull requests. Only a couple of rules are enforced to maintain clarity:

 1. Follow the [Conventional Commits](https://www.conventionalcommits.org/) specification. Type is mandatory, Scope and Summary are optional, but make sure to use ! or BREAKING CHANGE when needed.
 
 2.  Make a new branch for each distinct/independent change you work on. Use the format Type/NAME where Type matches the one in Conventional Commits.	
 `git checkout -b feature/oauth2-implementation`
 `git checkout -b refactor/simplify-token-generation`

3. Open a Pull Request explaining the problem solved or feature made, if exists, append screenshot of visual modifications and await review.


