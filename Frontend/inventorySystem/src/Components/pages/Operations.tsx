import { useEffect, useState } from "react";

type Operation = {
  operationId: number;
  productId: string;
  productName: string;
  operationQuantity: number;
  operationType: string;
  operationAt: string
};

const Operations = () => {
  const [operations, setOperations] = useState<Operation[]>([]);

  useEffect(() => {
    fetch("http://localhost:9000/v1/operations?pageNumber=1&pageSize=10")
      .then((response) => {
        if (!response.ok) throw new Error("Erro na requisição");
        return response.json();
      })
      .then((data) => setOperations(data.items))
      .catch((error) => console.error("Erro:", error));
  }, []);

  return (
    <main>
      <table>
        <thead>
          <tr>
            <th>Nome</th>
            <th>Quantidade</th>
            <th>Tipo de operação</th>
            <th>Data da operação</th>
          </tr>
        </thead>
        <tbody>
          {operations.map((operation) => {
            const translatedType = operation.operationType === "StockIn" ? "Entrada" : "Saída";

            const date = new Date(operation.operationAt);
            const formattedDateTime = `${String(date.getDate()).padStart(2, '0')}/${String(date.getMonth() + 1).padStart(2, '0')
              }/${date.getFullYear()} - ${String(date.getHours()).padStart(2, '0')}:${String(date.getMinutes()).padStart(2, '0')
              }`;

            return (
              <tr key={operation.operationId}>
                <td>{operation.productName}</td>
                <td>{operation.operationQuantity}</td>
                <td>{translatedType}</td>
                <td>{formattedDateTime}</td>
              </tr>
            );
          })}
        </tbody>
      </table>
    </main>
  );
};

export default Operations;
