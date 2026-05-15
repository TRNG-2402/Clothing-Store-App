import { useState } from 'react';
import { Link, Navigate, useNavigate } from 'react-router-dom';
import axios from 'axios'
import styles from './Login.module.css';

import NavBar from '../components/NavBar';

import { authService } from '../services/authService';
import { useAuth } from '../context/AuthContext';

export default function Login()
{

  const navigate = useNavigate();

  const {
    login,
    isAuthenticated,
  } = useAuth();

  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');

  const [error, setError] = useState('');

  // already logged in
  if (isAuthenticated)
  {
    return <Navigate to="/" replace />;
  }

  const handleSubmit = async (e: React.FormEvent) =>
  {
    e.preventDefault();

    setError('');

    try
    {
      const response = await authService.login({
        email,
        password,
      });

      login(response);

      navigate('/');
    }
    catch (err)
    {
      if (axios.isAxiosError(err))
      {
        setError(err.response?.data?.message ?? 'Login failed');
      } else
      {
        setError('Login failed');
      }
    }
  };

  return (
    <div>

      <NavBar />

      <div className={styles.container}>

        <form className={styles.form} onSubmit={handleSubmit}>

          <h2 className={styles.title}>Login</h2>

          <div className={styles.group}>
            <label>Email</label>
            <input
              className={styles.input}
              type="email"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
            />
          </div>

          <div className={styles.group}>
            <label>Password</label>
            <input
              className={styles.input}
              type="password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
            />
          </div>

          {error && (
            <p className={styles.error}>
              {error}
            </p>
          )}

          <button className={styles.button} type="submit">
            Login
          </button>

          <p className={styles.link}>
            Don't have an account?{' '}
            <Link to="/register">Register</Link>
          </p>

        </form>

      </div>

    </div>
  );
}