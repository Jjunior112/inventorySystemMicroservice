import { useEffect, useState } from "react";
import { Link } from "react-router-dom";

type Stock = {
  stockId: number;
  productId: number;
  productName: string;
  productCategory: string;
  productQuantity: number;
};

const Products = () => {
  const [stocks, setStocks] = useState<Stock[]>([]);

  useEffect(() => {
    fetch("http://localhost:9000/v1/stocks?pageNumber=1&pageSize=10")
      .then((response) => {
        if (!response.ok) throw new Error("Erro na requisição");
        return response.json();
      })
      .then((data) => setStocks(data.items))
      .catch((error) => console.error("Erro:", error));
  }, []);

  return (
    <main>
      <table>
        <thead>
          <tr>
            <th>Nome</th>
            <th>Categoria</th>
            <th>Saldo</th>
            <th>
              <button><Link to="newProduct">Cadastrar produto</Link></button>
            </th>
          </tr>
        </thead>
        <tbody>
          {stocks.map((stock) => (
            <tr key={stock.stockId}>
              <td>{stock.productName}</td>
              <td>{stock.productCategory}</td>
              <td>{stock.productQuantity}</td>
              <td>
                <div>
                  <button><Link to={`/operations/newOperation/${stock.stockId}`}>Nova operação</Link></button>
                  
                
                </div>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </main>
  );
};

export default Products;
