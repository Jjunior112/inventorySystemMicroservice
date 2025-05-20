const Home = () => {
  return (
    <main>
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
    </main>
  );
};

export default Home;
