import { useEffect, useState } from 'react'

import NavBar from '../components/NavBar'
import ProductCard from '../components/ProductCard'

import reactLogo from '../assets/react.svg'
import viteLogo from '../assets/vite.svg'
import heroImg from '../assets/hero.png'
import styles from './Home.module.css'
import type { RecommendationResponse } from '../services/recommendationService'
import { getRecommendations } from '../services/recommendationService'

export default function Home()
{
  const [recommendations, setRecommendations] =
    useState<RecommendationResponse | null>(null)

  const [loading, setLoading] = useState(true)
  const [error, setError] = useState('')

  useEffect(() =>
  {
    navigator.geolocation.getCurrentPosition(
      async (position) =>
      {
        try
        {
          const lat = position.coords.latitude
          const lon = position.coords.longitude

          const data = await getRecommendations(lat, lon)
          setRecommendations(data)
        } catch (err)
        {
          console.error(err)
          setError('Could not load recommendations.')
        } finally
        {
          setLoading(false)
        }
      },
      () =>
      {
        setError('Location permission denied.')
        setLoading(false)
      }
    )
  }, [])

  return (
    <div>
      <NavBar />

      {/* HERO SECTION */}
      <section id="center">
        <div className="hero">
          <img
            src={heroImg}
            className="base"
            width="170"
            height="179"
            alt=""
          />
          <img
            src={reactLogo}
            className="framework"
            alt="React logo"
          />
          <img src={viteLogo} className="vite" alt="Vite logo" />
        </div>

        <div>
          <h1>Clothing Store</h1>
        </div>
      </section>

      {/* RECOMMENDATIONS */}
      <section className={styles.recommendationSection}>
        <h2 className={styles.title}>Recommended for Your Weather</h2>

        {loading && <p>Loading recommendations...</p>}

        {error && <p>{error}</p>}

        {!loading && !recommendations && !error && (
          <p>No recommendations available.</p>
        )}

        {recommendations && (
          <>
            <div className={styles.meta}>
              <p>{recommendations.weatherSummary}</p>
              <p>
                <strong>Category:</strong>{' '}
                {recommendations.category}
              </p>
            </div>

            <div className={styles.productGrid}>
              {recommendations.products.map((product) => (
                <ProductCard
                  key={product.id}
                  product={product}
                />
              ))}
            </div>
          </>
        )}
      </section>
    </div>
  )
}