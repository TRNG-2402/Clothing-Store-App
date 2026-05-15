import { useEffect, useState } from 'react';
import { useNavigate, Link } from 'react-router-dom';

import NavBar from '../components/NavBar';
import { userService } from '../services/userService';
import type { UserProfile } from '../types/User';
import { useAuth } from '../context/AuthContext';

import styles from './Profile.module.css';

export default function Profile()
{

  const { logout } = useAuth();
  const navigate = useNavigate();

  const [profile, setProfile] = useState<UserProfile | null>(null);

  useEffect(() =>
  {
    const loadProfile = async () =>
    {
      try
      {
        const data = await userService.getMe();
        setProfile(data);
      } catch (err)
      {
        console.error(err);
      }
    };

    loadProfile();
  }, []);

  const handleLogout = () =>
  {
    logout();
    navigate('/login');
  };

  return (
    <div>
      <NavBar />
      <div className={styles.container}>

        <div className={styles.card}>

          <h2 className={styles.title}>Profile</h2>

          {!profile ? (
            <p>Loading...</p>
          ) : (
            <div className={styles.info}>
              <div>
                <span className={styles.label}>Name:</span> {profile.name}
              </div>

              <div>
                <span className={styles.label}>Email:</span> {profile.email}
              </div>
            </div>
          )}

          <div className={styles.actions}>

            <Link to="/profile/orders" className={styles.link}>
              View Orders
            </Link>

            <Link to="/cart" className={styles.link}>
              View Cart
            </Link>

            <button className={styles.button} onClick={handleLogout}>
              Logout
            </button>

          </div>

        </div>

      </div>

    </div>

  );
}