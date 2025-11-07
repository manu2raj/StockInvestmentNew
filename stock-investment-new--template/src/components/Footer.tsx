
import React from 'react';
import { Box, Typography } from '@mui/material';

const Footer: React.FC = () => (
  <Box sx={{ bgcolor: 'primary.main', color: 'white', p: 2, mt: 4, textAlign: 'center' }}>
    <Typography variant="body2">© 2025 Student Portal. All rights reserved.</Typography>
  </Box>
);

export default Footer;
