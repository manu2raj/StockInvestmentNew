import React from 'react';
import { AppBar, Toolbar, Typography, Button } from '@mui/material';
import { Link } from 'react-router-dom';

const Header: React.FC = () => (
  <AppBar position="static">
    <Toolbar>
      <Typography variant="h6" sx={{ flexGrow: 1 }}>
        Student Portal
      </Typography>
      <Button color="inherit" component={Link} to="/">Home</Button>
        <Button color="inherit" component= {Link} to="/login">Login</Button>
      <Button color="inherit" component={Link} to="/students">Students</Button>
      <Button color="inherit" component={Link} to="/register">Regsiter</Button>
   
    </Toolbar>
  </AppBar>
);

export default Header;