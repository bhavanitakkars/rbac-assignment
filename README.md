
# RBAC Assignment

This project implements a Role-Based Access Control (RBAC) system with a .NET backend and an Angular frontend.

## Project Structure

- **backend/**: ASP.NET Core Web API for authentication, user management, and content access.
- **frontend/rbac-ui/**: Angular application for user interface and interaction with the backend API.

## Backend
- Built with ASP.NET Core (.NET 8)
- JWT-based authentication
- In-memory user service for demo purposes
- Controllers for Auth, User, and Content management
- Configuration files: `appsettings.json`, `appsettings.Development.json`

### Running the Backend
1. Navigate to the `backend` directory.
2. Run the following command to start the API:
   ```powershell
   dotnet run
   ```
3. The API will be available at `http://localhost:5000` (or as configured).

## Frontend
- Built with Angular 19
- Uses Angular Material for UI components
- JWT decoding for authentication
- SSR (Server-Side Rendering) support with Express

### Running the Frontend
1. Navigate to `frontend/rbac-ui`.
2. Install dependencies:
   ```powershell
   npm install
   ```
3. Start the development server:
   ```powershell
   npm start
   ```
4. The app will be available at `http://localhost:4200`.

### SSR (Server-Side Rendering)
To run the SSR server:
```powershell
npm run build ; npm run serve:ssr:rbac-ui
```

## Features
- User login and JWT authentication
- Role-based access to content
- Angular Material UI
- Demo user management

## Development
- Backend: C# (.NET 8)
- Frontend: Angular 19, TypeScript

## License
This project is for educational/demo purposes.
