import { useEffect, useState } from "react";

type Stock = {
  productId: number;
  productName: string;
  productCategory: string;
  productQuantity: number;
};

const Table = () => {
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
              <button>Cadastrar produto</button>
            </th>
          </tr>
        </thead>
        <tbody>
          {stocks.map((product) => (
            <tr key={product.productId}>
              <td>{product.productName}</td>
              <td>{product.productCategory}</td>
              <td>{product.productQuantity}</td>
              <td>
                <div>
                  <button>Nova operação</button>
                  <button>Editar produto</button>
                  <button>Excluir produto</button>
                </div>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </main>
  );
};

export default Table;
