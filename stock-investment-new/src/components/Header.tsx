import React from 'react';
import { AppBar, Toolbar, Typography, Button, Box, Stack } from '@mui/material';
import { Link } from 'react-router-dom';

const Header: React.FC = () => (
  <Box sx={{ flexGrow: 1 }}>  
    <AppBar position="static">
      <Toolbar>        
        <Typography component={Link} to="/" variant="h6" sx={{
              mr: 2,
              display: { md: 'flex' },
              fontFamily: 'monospace',
              fontWeight: 700,
              letterSpacing: '.1rem',
              color: 'white',
              textDecoration: 'none',
            }}>
          Student Portal
        </Typography>
        <Stack  direction="row" justifyContent="flex-end"> 
          <Button color="inherit" component={Link} to="/">Home</Button>
          <Button color="inherit" component= {Link} to="/login">Login</Button>
          <Button color="inherit" component={Link} to="/register">Regsiter</Button>          
          <Button color="inherit" component={Link} to="/students">Students</Button>
          <Button color="inherit" component={Link} to="/dashborad">Stocks</Button>
        </Stack >
       
    
      </Toolbar>
    </AppBar>
  </Box>
);

export default Header;