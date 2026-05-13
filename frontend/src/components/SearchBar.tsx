import styles from './SearchBar.module.css'


interface SearchBarProps
{
    value: string,
    onSearchChange: (next: string) => void
}

export default function SearchBar({ value, onSearchChange }: SearchBarProps)
{
    return (
        <input
            type='text'
            placeholder='Search Clothes'
            value={value}
            onChange={(e) => onSearchChange(e.target.value)}
            className={styles.search}
        />
    )

}