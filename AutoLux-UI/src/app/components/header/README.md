## Bootstrap Navbar Header Component

I've successfully created a comprehensive navbar header component for your Angular application with the following features:

### Features Implemented:

1. **Bootstrap 5 Integration**

   - Added Bootstrap CSS and JavaScript via CDN in `index.html`
   - Added Bootstrap Icons for beautiful iconography

2. **Authentication-Aware Navbar**

   - Shows different content based on authentication status
   - Uses Angular signals for reactive state management

3. **For Unauthenticated Users:**

   - Sign In link with icon
   - Register link with icon

4. **For Authenticated Users:**

   - Welcome message with username
   - Dropdown menu with user options
   - Sign out functionality

5. **Navigation Links**

   - Products link (only visible when authenticated)
   - Brand logo/name linking to home

6. **Responsive Design**
   - Mobile-friendly with Bootstrap's responsive navbar
   - Collapsible menu for smaller screens
   - Hover effects and smooth transitions

### Files Created/Updated:

1. **Header Component (`header.ts`)**

   - Injected AuthService
   - Added computed signals for authentication state
   - Added sign out method

2. **Header Template (`header.html`)**

   - Bootstrap navbar structure
   - Conditional rendering based on auth status
   - Dropdown menu for authenticated users

3. **Header Styles (`header.css`)**

   - Custom styling for enhanced appearance
   - Hover effects and transitions
   - Mobile responsiveness

4. **App Integration**
   - Updated `app.html` to use the header component
   - Updated `app.ts` to import the header component
   - Added Bootstrap CDN links to `index.html`

### Usage:

The navbar will automatically:

- Show "Sign In" and "Register" links when user is not authenticated
- Show "Welcome, [username]" with a dropdown containing "Sign Out" when authenticated
- Display the Products link only for authenticated users
- Handle sign out functionality through the AuthService

The navbar is fully responsive and follows Bootstrap design patterns with custom enhancements for better user experience.
