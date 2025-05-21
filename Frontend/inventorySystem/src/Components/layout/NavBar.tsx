import { Link } from "react-router-dom"

const NavBar = () => {
    return (
        <nav>
            <ul>
                <li><Link to="/">Pagina inicial</Link></li>
                <li><Link to="/products">Produtos</Link></li>
                <li><Link to="/operations">Operações</Link></li>
           

        </ul>

        </nav >
    )
}

export default NavBar