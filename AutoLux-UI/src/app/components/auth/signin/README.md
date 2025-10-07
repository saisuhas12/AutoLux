# Signin Component with Bootstrap Tabs

This component provides a comprehensive authentication interface using Bootstrap 5 with tabbed navigation for both signin and registration functionality.

## Features

### 🎨 **UI/UX Features**

- **Bootstrap 5 Styling** - Modern, responsive design
- **Tabbed Interface** - Easy switching between Sign In and Register
- **Beautiful Animations** - Smooth transitions and hover effects
- **Icons Integration** - Bootstrap Icons for visual appeal
- **Gradient Background** - Professional gradient background
- **Form Validation** - Real-time validation with visual feedback

### 🔐 **Authentication Features**

- **Sign In Functionality** - Login with username/password
- **Registration** - Create new user accounts
- **Password Confirmation** - Validates matching passwords
- **Form Validation** - Client-side validation with error messages
- **Loading States** - Visual feedback during API calls
- **Success/Error Messages** - Clear feedback for user actions

### 📱 **Responsive Design**

- Mobile-friendly layout
- Responsive card design
- Touch-friendly buttons and inputs
- Optimized for all screen sizes

## Usage

### Default Route

The application now defaults to the signin page (`/signin`) for unauthenticated users.

### Navigation

- **Header Link**: "Sign In / Register" - leads to the signin page
- **Tab Navigation**: Switch between Sign In and Register within the same page
- **Auto-redirect**: Successful registration switches to Sign In tab
- **Post-login**: Successful signin redirects to `/products`

### Form Validation

Both forms include comprehensive validation:

- **Username**: Required, minimum 3 characters
- **Password**: Required, minimum 6 characters
- **Confirm Password**: Required, must match password

### Error Handling

- Network errors display user-friendly messages
- Form validation errors show field-specific feedback
- Loading states prevent multiple submissions

## Technical Implementation

### Components Structure

```
signin/
├── signin.ts         # Component logic with form handling
├── signin.html       # Bootstrap template with tabs
├── signin.css        # Custom styling and animations
└── README.md         # This documentation
```

### Key Technologies

- **Angular Reactive Forms** - Type-safe form handling
- **Bootstrap 5** - UI framework and responsive grid
- **Bootstrap Icons** - Icon library
- **RxJS** - Reactive programming for API calls
- **Angular Signals** - Modern state management

### Security Features

- **JWT Token Storage** - Secure token management
- **Route Guards** - Protected routes for authenticated users
- **Form Validation** - Client-side security validation
- **Error Handling** - Secure error message display

## Authentication Flow

1. **Unauthenticated User**

   - Redirected to `/signin`
   - Can choose Sign In or Register tab
   - Form validation prevents invalid submissions

2. **Registration Process**

   - Fill registration form
   - Successful registration shows success message
   - Auto-switches to Sign In tab after 1.5 seconds

3. **Sign In Process**

   - Fill signin form
   - Successful login shows success message
   - Redirects to `/products` after 1 second

4. **Authenticated User**
   - Can access protected routes
   - Header shows welcome message and logout option
   - Products page becomes accessible

## Guard Protection

The `authGuard` protects the products route:

- Checks authentication status
- Redirects unauthenticated users to `/signin`
- Allows authenticated users to access protected content

This ensures a secure and user-friendly authentication experience!
