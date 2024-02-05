from fluent.syntax import ast
from fluentast import FluentAstAbstract
from pydash import py_


class FluentAstComparer:
    def __init__(self, sourse_parsed: ast.Resource, target_parsed: ast.Resource):
        self.sourse_parsed = sourse_parsed
        self.target_parsed = target_parsed
        self.source_elements = list(
            filter(lambda el: el, list(map(lambda e: FluentAstAbstract.create_element(e), sourse_parsed.body))))
        self.target_elements = list(
            filter(lambda el: el, list(map(lambda e: FluentAstAbstract.create_element(e), target_parsed.body))))

    # Retorna mensagens completamente equivalentes (sem incluir span)
    def get_equal_elements(self):
        comparator = lambda a, b: a.element.equals(b.element, ignored_fields=['span'])

        return py_.intersection_with(self.source_elements, self.target_elements, comparator=comparator)

    # Retorna mensagens completamente não equivalentes (sem incluir span)
    def get_not_equal_elements(self):
        comparator = lambda a, b: a.element.equals(b.element, ignored_fields=['span'])
        diff = py_.difference_with(self.source_elements, self.target_elements, comparator=comparator)

        return diff

    # Retorna mensagens com nomes de chaves equivalentes
    def get_equal_id_names(self):
        comparator = lambda a, b: a.element.equals(b.element, ignored_fields=['span', 'value', 'comment', 'attributes'])
        eq = py_.intersection_with(self.source_elements, self.target_elements, comparator=comparator)

        return eq

    # Retorna mensagens com nomes de chaves não equivalentes
    def get_not_equal_id_names(self):
        comparator = lambda a, b: a.element.equals(b.element, ignored_fields=['span', 'value', 'comment', 'attributes'])
        diff = py_.difference_with(self.source_elements, self.target_elements, comparator=comparator)

        return diff

    # Retorna mensagens de destino existentes na origem
    def get_exist_id_names(self, source, target):
        comparator = lambda a, b: a.element.equals(b.element, ignored_fields=['span', 'value', 'comment', 'attributes'])
        eq = py_.intersection_with(source, target, comparator=comparator)

        return eq

    # Retorna mensagens de destino existentes na origem
    def get_not_exist_id_names(self):
        comparator = lambda a, b: a.element.equals(b.element, ignored_fields=['span', 'value', 'comment', 'attributes'])
        diff = py_.difference_with(self.target_elements, self.source_elements, comparator=comparator)

        return diff

    # Retorna mensagens com valores e atributos equivalentes
    def get_equal_values_with_attrs(self):
        comparator = lambda a, b: a.element.equals(b.element, ignored_fields=['span', 'id', 'comment'])
        eq = py_.intersection_with(self.target_elements, self.source_elements, comparator=comparator)

        return eq

    # Retorna mensagens da origem com valores e atributos não equivalentes
    def get_not_equal_values_with_attrs(self):
        comparator = lambda a, b: a.element.equals(b.element, ignored_fields=['span', 'id', 'comment'])
        diff = py_.difference_with(self.source_elements, self.target_elements,
                                   comparator=lambda a, b: a.element.equals(b.element,
                                                                            ignored_fields=['span', 'id', 'comment']))

        return diff

    # Retorna mensagens da origem que existem no destino e na origem, com valores e atributos não equivalentes
    def get_not_equal_exist_values_with_attrs(self):
        diff = py_.difference_with(self.source_elements, self.target_elements,
                                   comparator=lambda a, b: a.element.equals(b.element,
                                                                            ignored_fields=['span', 'id', 'comment']))
        ex = self.get_exist_id_names(self.source_elements, self.target_elements)
        exist = py_.intersection(diff, ex)

        return exist

        # Retorna mensagens do destino com valores e atributos não equivalentes

    def get_target_not_equal_values_with_attrs(self):
        comparator = lambda a, b: a.element.equals(b.element, ignored_fields=['span', 'id', 'comment'])
        diff = py_.difference_with(self.source_elements, self.target_elements, comparator=comparator)

        return diff

    # Retorna mensagens existentes no destino e na origem com valores e atributos não equivalentes
    def get_target_not_equal_exist_values_with_attrs(self):
        diff = py_.difference_with(self.target_elements, self.source_elements,
                                   comparator=lambda a, b: a.element.equals(b.element,
                                                                            ignored_fields=['span', 'id', 'comment']))
        exist = py_.intersection(diff, self.get_exist_id_names(self.target_elements, self.source_elements))

        return exist

    def find_message_by_id_name(self, id_name, list):
        return py_.find(list, lambda el: el.get_id_name() == id_name)
