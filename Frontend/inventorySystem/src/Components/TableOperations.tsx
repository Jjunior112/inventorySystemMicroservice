import { useEffect, useState } from "react";

type Operation = {
  operationId: number;
  productId: string;
  operationQuantity: number;
  operationType: number;
};

const TableOperations = () => {
  const [operations, setOperations] = useState<Operation[]>([]);

  useEffect(() => {
    fetch('http://localhost:9000/v1/operations?pageNumber=1&pageSize=10')
      .then(response => {
        if (!response.ok) throw new Error('Erro na requisição');
        return response.json();
      })
      .then(data => setOperations(data.items))
      .catch(error => console.error('Erro:', error));
  }, []);

  return (
    <table>
      <thead>
        <tr>
          <th>Nome</th>
          <th>Quantidade</th>
          <th>Tipo de operação</th>

        </tr>
      </thead>
      <tbody>
        {operations.map((operation) => (
          <tr key={operation.operationId}>
            <td>{operation.operationId}</td>
             <td>{operation.operationQuantity}</td>
            <td>{operation.operationType}</td>

           
          </tr>
        ))}
      </tbody>
    </table>
  );
};

export default TableOperations;
