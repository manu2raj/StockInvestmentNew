
import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import { Container } from '@mui/material';
import Header from './components/Header';
import Footer from './components/Footer';
import StudentList from './components/StudentList';

const App: React.FC = () => (
  <Router>
    <Header />
    <Container>
      <Routes>
        <Route path="/" element={<h2>Welcome to the Student Portal</h2>} />
        <Route path="/students" element={<StudentList />} />
      </Routes>
    </Container>
    <Footer />
  </Router>
);

export default App;