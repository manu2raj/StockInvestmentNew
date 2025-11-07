
import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import { Container } from '@mui/material';
import Header from './components/Header';
import Footer from './components/Footer';
import StudentList from './components/students/StudentList';
import Login from './components/LoginRegister/Login';
import Register from './components/LoginRegister/Register';
import { Dashboard } from '@mui/icons-material';

const App: React.FC = () => (
  <Router>
    <Header />
    <Container maxWidth="md">
      <Routes>
        <Route path="/" element={<h2>Welcome to the Student Portal</h2>} />
        <Route path="/students" element={<StudentList />} />
        <Route path='/login' element={<Login/>} />
        <Route path="/register" element={<Register/>} />
        <Route path="/dashborad" element={<Dashboard/>} />
      </Routes>
    </Container>
    <Footer />
  </Router>
);

export default App;