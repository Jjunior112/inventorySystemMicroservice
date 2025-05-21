import { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";


const AddOperationForm = () => {

  const { stockId } = useParams();


  const navigate = useNavigate();

  const [productId, setProductId] = useState('');
  const [productName, setProductName] = useState('');
  const [productCategory, setProductCategory] = useState('');
  const [operationQuantity, setOperationQuantity] = useState('');
  const [operationType, setOperationType] = useState(3);


  const [loading, setLoading] = useState(false);

  useEffect(() => {
    fetch(`http://localhost:9000/v1/stocks/${stockId}`)
      .then((response) => {
        if (!response.ok) throw new Error("Erro na requisição");
        return response.json();
      })
      .then((data) => {
        setProductId(data.productId)
        setProductName(data.productName)
        setProductCategory(data.productCategory)
      })
      .catch((error) => console.error("Erro:", error));
  }, []);



  const handleSubmit = async (e) => {
    e.preventDefault();

    setLoading(true)

    try {
      const response = await fetch(`http://localhost:9000/v1/operations`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          productId: productId,
          operationQuantity: operationQuantity,
          operationType: operationType
        })
      });


      if (response.status !== 201 || operationType ===3) {
        throw new Error('Produto não encontrado ou erro ao criar operação!');
        
      } else {
        alert('operação efetuada com sucesso!');
        navigate('/products');
      }


    } catch (error) {
      console.log(error.message)
    } finally {
      setLoading(false);
    }
  };

  return (
    <main>
      <h1>Registrar Operação</h1>

      <form id="operationForm" onSubmit={handleSubmit}>

        <label htmlFor="productName">Nome:</label>
        <input
          type="text"
          id="productName"
          name="productName"
          value={productName}
          readOnly
        />
        <label htmlFor="productCategory">Categoria:</label>
        <input
          type="text"
          id="productCategory"
          name="productCategory"
          value={productCategory}
          readOnly
        />

        <label htmlFor="operationQuantity">Quantidade:</label>
        <input
          type="number"
          id="operationQuantity"
          name="operationQuantity"
          onChange={(e) => setOperationQuantity(Number(e.target.value))}
          required
        />


        <label htmlFor="operationType">Categoria:</label>

        <select
          id="operationType"
          name="operationType"
          required
          value={operationType}
          onChange={(e) => setOperationType(Number(e.target.value))}
        >
          <option value="3">Selecione uma operação</option>
          <option value="1">Entrada</option>
          <option value="0">Saída</option>
        </select>

        <button type="submit" disabled={loading}>
          {loading ? 'Registrando...' : 'Registrar operação'}
        </button>
      </form>
    </main>
  );
};

export default AddOperationForm;
