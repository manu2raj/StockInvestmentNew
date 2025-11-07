import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { login } from '../../services/AuthServices';
import { Container, Box, Typography, TextField, FormControlLabel, Checkbox, Button } from '@mui/material';

const Login: React.FC = () => {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const navigate = useNavigate();

  const handleLogin = async (e: React.FormEvent) => {
    e.preventDefault();//
    try {
      await login(username, password);
      alert('Login successful!');
      navigate('/dashboard'); // or wherever you want to redirect
    } catch {
      // Error already handled in authService
    }
  };

  return (
    <Container component="main" maxWidth="xs" onSubmit={handleLogin}>
      <Box
        sx={{
          marginTop: 8,
          display: 'flex',
          flexDirection: 'column',
          alignItems: 'center',
        }}
      >
        <Typography component="h1" variant="h5">
          Sign In
        </Typography>
        <Box component="form" 
        //onSubmit={handleSubmit} sx={{ mt: 1 }}
        >
          <TextField
            margin="normal"
            required
            fullWidth
            id="username"
            label="Username"
            name="username"
            autoComplete="username"
            autoFocus
            onChange={e => setUsername(e.target.value)}
            // value={email}
            //onChange={(e) => setEmail(e.target.value)}
          />
          <TextField
            margin="normal"
            required
            fullWidth
            name="password"
            label="Password"
            type="password"
            id="password"
            autoComplete="current-password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
          />
          <FormControlLabel
            control={<Checkbox value="remember" color="primary" />}
            label="Remember me"
          />
          <Button
            type="submit"
            fullWidth
            variant="contained"
            sx={{ mt: 3, mb: 2 }}
          >
            Sign In
          </Button>        
        </Box>
      </Box>
    </Container>
  );
};

export default Login;
