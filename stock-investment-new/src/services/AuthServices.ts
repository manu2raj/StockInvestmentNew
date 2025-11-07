import axios from 'axios';

const API_URL = 'https://localhost:44361/api/StudentsAlls';

export const login = async (username: string, password: string) => {
  const res = await axios.post(`${API_URL}/login`, { username, password });
  localStorage.setItem('token', res.data.token);
};

export const register = async (username: string, password: string) => {
  await axios.post(`${API_URL}/register`, { username, password });
};

export const getToken = () => localStorage.getItem('token');