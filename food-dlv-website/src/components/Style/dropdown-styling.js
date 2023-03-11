import styled from "styled-components";

// 使用時要在這個 Trigger 再加一個 className="group-hover:block"
export const Trigger = styled.div`
  position: absolute;
  z-index: 10;
  display: none;
`;

//寬度需自己設定
export const DropdownMenu = styled.div`
  padding: 0.5rem 0;
  display: flex;
  justify-content: center;
  background-color: white;
`;

export const DropdownItem = styled.li`
  padding: 0.5rem 0;
  transition-property: color, background-color, border-color,
    text-decoration-color, fill, stroke;
  transition-timing-function: cubic-bezier(0.4, 0, 0.2, 1);
  transition-duration: 300ms;
  &:hover {
    background-color: rgb(228, 228, 231);
    color: rgb(5, 150, 105);
  }
`;
