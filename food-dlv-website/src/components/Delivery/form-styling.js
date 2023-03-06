import styled from "styled-components";

export const Input = styled.input`
  display: block;
  width: 100%;
  padding: 0.5rem 1rem;
  margin-top: 0.5rem;
  --tw-text-opacity: 1;
  color: rgb(64, 64, 64);
  background-color: white;
  border-width: 1px;
  border-radius: 0.375rem;
  transition: 0.5s;
  &:focus {
    box-shadow: 0 0 5pt 2pt #d3d3d3;
    outline-width: 0px;
  }
`;

export const Label = styled.label`
  display: block;
  font-size: 1rem;
  line-height: 1.5rem;
  font-weight: 600;
  color: rgb(31 41 55 / var(--tw-text-opacity));
`;
