import { useState } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import axios from 'axios';
import styles from './Register.module.css';

import NavBar from '../components/NavBar';
import { authService } from '../services/authService';

export default function Register()
{

    const navigate = useNavigate();

    const [name, setName] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');

    const handleSubmit = async (e: React.FormEvent) =>
    {
        e.preventDefault();

        setError('');

        try
        {
            await authService.register({
                name,
                email,
                password
            });

            // AFTER register -> go to login
            navigate('/login');
        }
        catch (err)
        {
            if (axios.isAxiosError(err))
            {
                setError(err.response?.data?.message ?? 'Registration failed');
            } else
            {
                setError('Registration failed');
            }
        }
    };

    return (
        <div>

            <NavBar />

            <div className={styles.container}>

                <form className={styles.form} onSubmit={handleSubmit}>

                    <h2 className={styles.title}>Register</h2>

                    <div className={styles.group}>
                        <label>Name</label>
                        <input
                            className={styles.input}
                            value={name}
                            onChange={(e) => setName(e.target.value)}
                        />
                    </div>

                    <div className={styles.group}>
                        <label>Email</label>
                        <input
                            className={styles.input}
                            value={email}
                            onChange={(e) => setEmail(e.target.value)}
                        />
                    </div>

                    <div className={styles.group}>
                        <label>Password</label>
                        <input
                            type="password"
                            className={styles.input}
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
                        Register
                    </button>

                    <p className={styles.link}>
                        Already have an account?{' '}
                        <Link to="/login">Login</Link>
                    </p>

                </form>

            </div>

        </div>
    );
}