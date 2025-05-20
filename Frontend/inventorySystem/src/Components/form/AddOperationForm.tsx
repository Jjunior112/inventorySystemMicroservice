const AddOperationForm = () => {
  return (
    <>
      <h1>Registrar Operação</h1>

      <form id="productForm">
        <label for="operationQuantity">Quantidade:</label>
        <input
          type="number"
          id="operationQuantity"
          name="operationQuantity"
          placeholder="Digite a quantidade da operação"
          required
        />

        <label for="operationType">Operação:</label>
        <br />
        <select id="operationType" name="operationType" required>
          <option value="">Selecione uma operação</option>
          <option value="entrada">Entrada</option>
          <option value="saida">Saída</option>
        </select>

        <button type="submit">Registrar operação</button>
      </form>
    </>
  );
};

export default AddOperationForm;
